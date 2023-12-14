PGDMP  +                    {         	   CustomDB2    16.0    16.0 [               0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                        0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            !           1262    16480 	   CustomDB2    DATABASE        CREATE DATABASE "CustomDB2" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "CustomDB2";
                postgres    false            �            1259    16569    application    TABLE     �   CREATE TABLE public.application (
    id integer NOT NULL,
    product_id integer NOT NULL,
    custom_post_id integer NOT NULL,
    staff_id integer,
    status_id integer NOT NULL,
    user_id integer NOT NULL,
    export_country_id integer NOT NULL
);
    DROP TABLE public.application;
       public         heap    postgres    false            �            1259    16810 
   app_id_seq    SEQUENCE     s   CREATE SEQUENCE public.app_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 !   DROP SEQUENCE public.app_id_seq;
       public          postgres    false    215            "           0    0 
   app_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.app_id_seq OWNED BY public.application.id;
          public          postgres    false    235            �            1259    16572    custom_post    TABLE     �   CREATE TABLE public.custom_post (
    customs_post_id integer NOT NULL,
    customs_post_title character varying(120) NOT NULL,
    location character varying(240) NOT NULL,
    throughput integer,
    fk_vehicle_id integer NOT NULL
);
    DROP TABLE public.custom_post;
       public         heap    postgres    false            �            1259    16575 !   customs_posts_customs_post_id_seq    SEQUENCE     �   CREATE SEQUENCE public.customs_posts_customs_post_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 8   DROP SEQUENCE public.customs_posts_customs_post_id_seq;
       public          postgres    false    216            #           0    0 !   customs_posts_customs_post_id_seq    SEQUENCE OWNED BY     e   ALTER SEQUENCE public.customs_posts_customs_post_id_seq OWNED BY public.custom_post.customs_post_id;
          public          postgres    false    217            �            1259    16721    export_countries    TABLE     t   CREATE TABLE public.export_countries (
    id bigint NOT NULL,
    country_title character varying(250) NOT NULL
);
 $   DROP TABLE public.export_countries;
       public         heap    postgres    false            �            1259    16720    export_countries_id_seq    SEQUENCE     �   CREATE SEQUENCE public.export_countries_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public.export_countries_id_seq;
       public          postgres    false    231            $           0    0    export_countries_id_seq    SEQUENCE OWNED BY     S   ALTER SEQUENCE public.export_countries_id_seq OWNED BY public.export_countries.id;
          public          postgres    false    230            �            1259    16579    history    TABLE     }   CREATE TABLE public.history (
    id integer NOT NULL,
    custom_date date NOT NULL,
    application_id integer NOT NULL
);
    DROP TABLE public.history;
       public         heap    postgres    false            �            1259    17242    history_id_seq    SEQUENCE     w   CREATE SEQUENCE public.history_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.history_id_seq;
       public          postgres    false    218            %           0    0    history_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.history_id_seq OWNED BY public.history.id;
          public          postgres    false    236            �            1259    16695    loggs    TABLE     w  CREATE TABLE public.loggs (
    id integer NOT NULL,
    datetime timestamp without time zone NOT NULL,
    user_id integer NOT NULL,
    action character varying(250) NOT NULL,
    "table" character varying(250) NOT NULL,
    attribute character varying(250) NOT NULL,
    meaning_before character varying(1000) NOT NULL,
    meaning_now character varying(1000) NOT NULL
);
    DROP TABLE public.loggs;
       public         heap    postgres    false            �            1259    16694    loggs_id_seq    SEQUENCE     �   CREATE SEQUENCE public.loggs_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.loggs_id_seq;
       public          postgres    false    229            &           0    0    loggs_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.loggs_id_seq OWNED BY public.loggs.id;
          public          postgres    false    228            �            1259    16587    product    TABLE     �   CREATE TABLE public.product (
    product_id integer NOT NULL,
    product_title character varying(120) NOT NULL,
    mass integer NOT NULL,
    fk_type_product_id integer NOT NULL
);
    DROP TABLE public.product;
       public         heap    postgres    false            �            1259    16590    product_type    TABLE     �   CREATE TABLE public.product_type (
    type_product_id integer NOT NULL,
    type_product_title character varying(30) NOT NULL,
    customs_clearance_coefficient integer NOT NULL
);
     DROP TABLE public.product_type;
       public         heap    postgres    false            �            1259    16593    products_product_id_seq    SEQUENCE     �   CREATE SEQUENCE public.products_product_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public.products_product_id_seq;
       public          postgres    false    219            '           0    0    products_product_id_seq    SEQUENCE OWNED BY     R   ALTER SEQUENCE public.products_product_id_seq OWNED BY public.product.product_id;
          public          postgres    false    221            �            1259    16594    staff    TABLE     q  CREATE TABLE public.staff (
    id integer NOT NULL,
    name character varying(250) NOT NULL,
    age integer NOT NULL,
    work_experience integer NOT NULL,
    custom_post_id integer NOT NULL,
    job_title character varying(250) NOT NULL,
    phone_number integer NOT NULL,
    email character varying(250) NOT NULL,
    password character varying(250) NOT NULL
);
    DROP TABLE public.staff;
       public         heap    postgres    false            �            1259    16773    staff_id_seq    SEQUENCE     u   CREATE SEQUENCE public.staff_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.staff_id_seq;
       public          postgres    false    222            (           0    0    staff_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.staff_id_seq OWNED BY public.staff.id;
          public          postgres    false    234            �            1259    16734    status    TABLE     i   CREATE TABLE public.status (
    id bigint NOT NULL,
    status_title character varying(250) NOT NULL
);
    DROP TABLE public.status;
       public         heap    postgres    false            �            1259    16733    status_id_seq    SEQUENCE     v   CREATE SEQUENCE public.status_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.status_id_seq;
       public          postgres    false    233            )           0    0    status_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.status_id_seq OWNED BY public.status.id;
          public          postgres    false    232            �            1259    16602 "   types_products_type_product_id_seq    SEQUENCE     �   CREATE SEQUENCE public.types_products_type_product_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 9   DROP SEQUENCE public.types_products_type_product_id_seq;
       public          postgres    false    220            *           0    0 "   types_products_type_product_id_seq    SEQUENCE OWNED BY     g   ALTER SEQUENCE public.types_products_type_product_id_seq OWNED BY public.product_type.type_product_id;
          public          postgres    false    223            �            1259    16603    user    TABLE     �   CREATE TABLE public."user" (
    user_id integer NOT NULL,
    user_name character varying(120) NOT NULL,
    login character varying(120),
    password character varying(20),
    role character varying(25)
);
    DROP TABLE public."user";
       public         heap    postgres    false            �            1259    16606    users_user_id_seq    SEQUENCE     �   CREATE SEQUENCE public.users_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.users_user_id_seq;
       public          postgres    false    224            +           0    0    users_user_id_seq    SEQUENCE OWNED BY     H   ALTER SEQUENCE public.users_user_id_seq OWNED BY public."user".user_id;
          public          postgres    false    225            �            1259    16607    vehicle_type    TABLE     �   CREATE TABLE public.vehicle_type (
    vehicle_type_id integer NOT NULL,
    vehicle_type_title character varying(30) NOT NULL
);
     DROP TABLE public.vehicle_type;
       public         heap    postgres    false            �            1259    16610 !   vehicle_types_vehicle_type_id_seq    SEQUENCE     �   CREATE SEQUENCE public.vehicle_types_vehicle_type_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 8   DROP SEQUENCE public.vehicle_types_vehicle_type_id_seq;
       public          postgres    false    226            ,           0    0 !   vehicle_types_vehicle_type_id_seq    SEQUENCE OWNED BY     f   ALTER SEQUENCE public.vehicle_types_vehicle_type_id_seq OWNED BY public.vehicle_type.vehicle_type_id;
          public          postgres    false    227            L           2604    17241    application id    DEFAULT     h   ALTER TABLE ONLY public.application ALTER COLUMN id SET DEFAULT nextval('public.app_id_seq'::regclass);
 =   ALTER TABLE public.application ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    235    215            M           2604    16611    custom_post customs_post_id    DEFAULT     �   ALTER TABLE ONLY public.custom_post ALTER COLUMN customs_post_id SET DEFAULT nextval('public.customs_posts_customs_post_id_seq'::regclass);
 J   ALTER TABLE public.custom_post ALTER COLUMN customs_post_id DROP DEFAULT;
       public          postgres    false    217    216            U           2604    16724    export_countries id    DEFAULT     z   ALTER TABLE ONLY public.export_countries ALTER COLUMN id SET DEFAULT nextval('public.export_countries_id_seq'::regclass);
 B   ALTER TABLE public.export_countries ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    230    231    231            N           2604    17245 
   history id    DEFAULT     h   ALTER TABLE ONLY public.history ALTER COLUMN id SET DEFAULT nextval('public.history_id_seq'::regclass);
 9   ALTER TABLE public.history ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    236    218            T           2604    16698    loggs id    DEFAULT     d   ALTER TABLE ONLY public.loggs ALTER COLUMN id SET DEFAULT nextval('public.loggs_id_seq'::regclass);
 7   ALTER TABLE public.loggs ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    228    229    229            O           2604    16612    product product_id    DEFAULT     y   ALTER TABLE ONLY public.product ALTER COLUMN product_id SET DEFAULT nextval('public.products_product_id_seq'::regclass);
 A   ALTER TABLE public.product ALTER COLUMN product_id DROP DEFAULT;
       public          postgres    false    221    219            P           2604    16802    product_type type_product_id    DEFAULT     �   ALTER TABLE ONLY public.product_type ALTER COLUMN type_product_id SET DEFAULT nextval('public.types_products_type_product_id_seq'::regclass);
 K   ALTER TABLE public.product_type ALTER COLUMN type_product_id DROP DEFAULT;
       public          postgres    false    223    220            Q           2604    16781    staff id    DEFAULT     d   ALTER TABLE ONLY public.staff ALTER COLUMN id SET DEFAULT nextval('public.staff_id_seq'::regclass);
 7   ALTER TABLE public.staff ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    234    222            V           2604    16737 	   status id    DEFAULT     f   ALTER TABLE ONLY public.status ALTER COLUMN id SET DEFAULT nextval('public.status_id_seq'::regclass);
 8   ALTER TABLE public.status ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    233    232    233            R           2604    16614    user user_id    DEFAULT     o   ALTER TABLE ONLY public."user" ALTER COLUMN user_id SET DEFAULT nextval('public.users_user_id_seq'::regclass);
 =   ALTER TABLE public."user" ALTER COLUMN user_id DROP DEFAULT;
       public          postgres    false    225    224            S           2604    16615    vehicle_type vehicle_type_id    DEFAULT     �   ALTER TABLE ONLY public.vehicle_type ALTER COLUMN vehicle_type_id SET DEFAULT nextval('public.vehicle_types_vehicle_type_id_seq'::regclass);
 K   ALTER TABLE public.vehicle_type ALTER COLUMN vehicle_type_id DROP DEFAULT;
       public          postgres    false    227    226                      0    16569    application 
   TABLE DATA           v   COPY public.application (id, product_id, custom_post_id, staff_id, status_id, user_id, export_country_id) FROM stdin;
    public          postgres    false    215   7k                 0    16572    custom_post 
   TABLE DATA           o   COPY public.custom_post (customs_post_id, customs_post_title, location, throughput, fk_vehicle_id) FROM stdin;
    public          postgres    false    216   �k                 0    16721    export_countries 
   TABLE DATA           =   COPY public.export_countries (id, country_title) FROM stdin;
    public          postgres    false    231   �r       	          0    16579    history 
   TABLE DATA           B   COPY public.history (id, custom_date, application_id) FROM stdin;
    public          postgres    false    218   �r                 0    16695    loggs 
   TABLE DATA           o   COPY public.loggs (id, datetime, user_id, action, "table", attribute, meaning_before, meaning_now) FROM stdin;
    public          postgres    false    229   Fs       
          0    16587    product 
   TABLE DATA           V   COPY public.product (product_id, product_title, mass, fk_type_product_id) FROM stdin;
    public          postgres    false    219   �|                 0    16590    product_type 
   TABLE DATA           j   COPY public.product_type (type_product_id, type_product_title, customs_clearance_coefficient) FROM stdin;
    public          postgres    false    220   �}                 0    16594    staff 
   TABLE DATA           y   COPY public.staff (id, name, age, work_experience, custom_post_id, job_title, phone_number, email, password) FROM stdin;
    public          postgres    false    222   b~                 0    16734    status 
   TABLE DATA           2   COPY public.status (id, status_title) FROM stdin;
    public          postgres    false    233   ��                 0    16603    user 
   TABLE DATA           K   COPY public."user" (user_id, user_name, login, password, role) FROM stdin;
    public          postgres    false    224   �                 0    16607    vehicle_type 
   TABLE DATA           K   COPY public.vehicle_type (vehicle_type_id, vehicle_type_title) FROM stdin;
    public          postgres    false    226   ��       -           0    0 
   app_id_seq    SEQUENCE SET     9   SELECT pg_catalog.setval('public.app_id_seq', 19, true);
          public          postgres    false    235            .           0    0 !   customs_posts_customs_post_id_seq    SEQUENCE SET     P   SELECT pg_catalog.setval('public.customs_posts_customs_post_id_seq', 44, true);
          public          postgres    false    217            /           0    0    export_countries_id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.export_countries_id_seq', 5, true);
          public          postgres    false    230            0           0    0    history_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.history_id_seq', 12, true);
          public          postgres    false    236            1           0    0    loggs_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.loggs_id_seq', 115, true);
          public          postgres    false    228            2           0    0    products_product_id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.products_product_id_seq', 73, true);
          public          postgres    false    221            3           0    0    staff_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.staff_id_seq', 21, true);
          public          postgres    false    234            4           0    0    status_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.status_id_seq', 6, true);
          public          postgres    false    232            5           0    0 "   types_products_type_product_id_seq    SEQUENCE SET     Q   SELECT pg_catalog.setval('public.types_products_type_product_id_seq', 20, true);
          public          postgres    false    223            6           0    0    users_user_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.users_user_id_seq', 82, true);
          public          postgres    false    225            7           0    0 !   vehicle_types_vehicle_type_id_seq    SEQUENCE SET     O   SELECT pg_catalog.setval('public.vehicle_types_vehicle_type_id_seq', 7, true);
          public          postgres    false    227            b           2606    16775    staff Staff_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.staff
    ADD CONSTRAINT "Staff_pkey" PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.staff DROP CONSTRAINT "Staff_pkey";
       public            postgres    false    222            X           2606    17235    application application_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.application
    ADD CONSTRAINT application_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.application DROP CONSTRAINT application_pkey;
       public            postgres    false    215            Z           2606    16621    custom_post customs_posts_pkey 
   CONSTRAINT     i   ALTER TABLE ONLY public.custom_post
    ADD CONSTRAINT customs_posts_pkey PRIMARY KEY (customs_post_id);
 H   ALTER TABLE ONLY public.custom_post DROP CONSTRAINT customs_posts_pkey;
       public            postgres    false    216            j           2606    16726 &   export_countries export_countries_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.export_countries
    ADD CONSTRAINT export_countries_pkey PRIMARY KEY (id);
 P   ALTER TABLE ONLY public.export_countries DROP CONSTRAINT export_countries_pkey;
       public            postgres    false    231            \           2606    17244    history history_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.history
    ADD CONSTRAINT history_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.history DROP CONSTRAINT history_pkey;
       public            postgres    false    218            h           2606    16700    loggs loggs_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.loggs
    ADD CONSTRAINT loggs_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.loggs DROP CONSTRAINT loggs_pkey;
       public            postgres    false    229            ^           2606    16627    product products_pkey 
   CONSTRAINT     [   ALTER TABLE ONLY public.product
    ADD CONSTRAINT products_pkey PRIMARY KEY (product_id);
 ?   ALTER TABLE ONLY public.product DROP CONSTRAINT products_pkey;
       public            postgres    false    219            l           2606    16739    status status_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.status
    ADD CONSTRAINT status_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.status DROP CONSTRAINT status_pkey;
       public            postgres    false    233            `           2606    16804     product_type types_products_pkey 
   CONSTRAINT     k   ALTER TABLE ONLY public.product_type
    ADD CONSTRAINT types_products_pkey PRIMARY KEY (type_product_id);
 J   ALTER TABLE ONLY public.product_type DROP CONSTRAINT types_products_pkey;
       public            postgres    false    220            d           2606    16633    user users_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);
 ;   ALTER TABLE ONLY public."user" DROP CONSTRAINT users_pkey;
       public            postgres    false    224            f           2606    16635    vehicle_type vehicle_types_pkey 
   CONSTRAINT     j   ALTER TABLE ONLY public.vehicle_type
    ADD CONSTRAINT vehicle_types_pkey PRIMARY KEY (vehicle_type_id);
 I   ALTER TABLE ONLY public.vehicle_type DROP CONSTRAINT vehicle_types_pkey;
       public            postgres    false    226            t           2606    17236    history applHist    FK CONSTRAINT     �   ALTER TABLE ONLY public.history
    ADD CONSTRAINT "applHist" FOREIGN KEY (application_id) REFERENCES public.application(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 <   ALTER TABLE ONLY public.history DROP CONSTRAINT "applHist";
       public          postgres    false    4696    218    215            m           2606    16728    application country_fk    FK CONSTRAINT     �   ALTER TABLE ONLY public.application
    ADD CONSTRAINT country_fk FOREIGN KEY (export_country_id) REFERENCES public.export_countries(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 @   ALTER TABLE ONLY public.application DROP CONSTRAINT country_fk;
       public          postgres    false    215    4714    231            n           2606    16636    application custom_post_fk    FK CONSTRAINT     �   ALTER TABLE ONLY public.application
    ADD CONSTRAINT custom_post_fk FOREIGN KEY (custom_post_id) REFERENCES public.custom_post(customs_post_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 D   ALTER TABLE ONLY public.application DROP CONSTRAINT custom_post_fk;
       public          postgres    false    4698    216    215            v           2606    16641    staff custom_post_fk    FK CONSTRAINT     �   ALTER TABLE ONLY public.staff
    ADD CONSTRAINT custom_post_fk FOREIGN KEY (custom_post_id) REFERENCES public.custom_post(customs_post_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 >   ALTER TABLE ONLY public.staff DROP CONSTRAINT custom_post_fk;
       public          postgres    false    222    216    4698            u           2606    16805    product fk_product_type    FK CONSTRAINT     �   ALTER TABLE ONLY public.product
    ADD CONSTRAINT fk_product_type FOREIGN KEY (fk_type_product_id) REFERENCES public.product_type(type_product_id) ON UPDATE CASCADE ON DELETE CASCADE;
 A   ALTER TABLE ONLY public.product DROP CONSTRAINT fk_product_type;
       public          postgres    false    220    4704    219            s           2606    16656    custom_post fk_vehicle_type_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.custom_post
    ADD CONSTRAINT fk_vehicle_type_id FOREIGN KEY (fk_vehicle_id) REFERENCES public.vehicle_type(vehicle_type_id) ON UPDATE CASCADE ON DELETE CASCADE;
 H   ALTER TABLE ONLY public.custom_post DROP CONSTRAINT fk_vehicle_type_id;
       public          postgres    false    216    4710    226            o           2606    16666    application product_fk    FK CONSTRAINT     �   ALTER TABLE ONLY public.application
    ADD CONSTRAINT product_fk FOREIGN KEY (product_id) REFERENCES public.product(product_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 @   ALTER TABLE ONLY public.application DROP CONSTRAINT product_fk;
       public          postgres    false    215    219    4702            p           2606    16740    application status_fk    FK CONSTRAINT     �   ALTER TABLE ONLY public.application
    ADD CONSTRAINT status_fk FOREIGN KEY (status_id) REFERENCES public.status(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 ?   ALTER TABLE ONLY public.application DROP CONSTRAINT status_fk;
       public          postgres    false    4716    233    215            q           2606    16776    application stuff_fk    FK CONSTRAINT     �   ALTER TABLE ONLY public.application
    ADD CONSTRAINT stuff_fk FOREIGN KEY (staff_id) REFERENCES public.staff(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 >   ALTER TABLE ONLY public.application DROP CONSTRAINT stuff_fk;
       public          postgres    false    4706    215    222            r           2606    16686    application user_fk    FK CONSTRAINT     �   ALTER TABLE ONLY public.application
    ADD CONSTRAINT user_fk FOREIGN KEY (user_id) REFERENCES public."user"(user_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 =   ALTER TABLE ONLY public.application DROP CONSTRAINT user_fk;
       public          postgres    false    224    4708    215               c   x�U�Q� ��r�ED���cuqf����	�T�B�#�ʃ���ΐ��A\a[����:��e�\pY�^ц�r/|���:�0�������	         �  x��X�rG}����ɮ�������|�(R�rI�b�
[F�I��T!�H�m���?���e`8��Tb�=ݧ���R�b(�"�b,b!_��#�"�y�w�ɮ�Ȏ��DLj��Cg�cژs�P^8�q/f��x84���Zs,�h�ˮ<��b����񚵊�h���[M*�]����?p`��=|>��zS���Q��Q���Fůz�u�K��W��65�I��9���9���y!��O�Q*&/�u�i��-n���k�wڪ��>ԫU�0���zΞH��#V>5��B;�Ԙ-Š�ŗ8�����C��Ȼ��/���#�=�ϱ�1��)N����ڛ��d��X�as,����v�8��pGVG�p����{��)��	�[�a�<%��m�mhڃ��]�3x��qkhZ��O����#���A�T��uB.'1'�����X���@����Q��3L��N���f�|LP@Fh~5���
�#:�<��4_)�A,�ʕ8�j:��bkFCb =��L�/��iN;�i��1	N[V��c�e�I�Ώ70�wv	��!G k�u<6�3$����.y�uè�U��NX!�[0(�Ֆ{���:9��|~b�)�n����=��g���7d������^�$Pd��fо�ZA@^����џ����t��+��C`c��\7 �h��]�J~�����(B�GZ�)xaZ�~�*pj�]�"�D��T��%��W����.�®D�Y��b?:G� ����m�V��M�m}�O�&hI�V,�^|�*����I�c�g�~����y����T����+;���N�JD���k�;8&9g���ٕ*��+��
��l�n-)���hڥ�7�s�Y���l������9{��(k������Jq�����t�]�z\�g�5�}G�ο�;�ò�	 ��eItSRi��7Y 7%kB��B8юpI��vm���甈9��:gɬ���V��N���f?�M�m�T{��<��尽������X%˼50�����U�2w^|FG�_�l��BM1e�ક+"f���|L]��[�]�r��ºSu'Q)����r��m�{J�V�M>�r�S;h���a�)��m|����<�K���K��Czv)��'�g׈��%�s�sb�Ն/
�3�xu�AG�ak�|\+���k�Mr��(`?xgٻ&��tZ�	H�Hq�;�K{���1�UV�9��$���:�y��7�a�I���'�<R�҅:�	���<?���Q�5Y����AɷX��s�s��.d-ߣ���t����=�HR�A��U���ՖOF۩=��y�����psT	�n�9�ȩ��/߷ܪo�B�}�Uյ~��f��gWڴ}Lٛ����X��ʚ�+�.�b��l1�>U��%hy����6���rʹ�%�j�q�"�j����FR�w�_ʓ�L��L��X�1X>�{8{l�H_��I���q�K�@U��$
d�'��< �1�Ȥ�-�s�ҭ#�$}(ʀQ�lv�6�gb`�v�+}�|�}��;���Y�m(�{�KW���YC4�\W,6<�is��+ݶ��\�Bf�C6��Ocz=��*b�w̬��K��4�I�.���vlf����4�����}4)�D�����[˗�%m4�����$VO��̄}�^=Kc��UiV��!�.t�Z;����qVv���q1�x���d~�0ݵ����Un��tA���j��7 �z�         K   x�3�0��M6\��e�l�������{��9/,�� b^l���2ἰ��raۅv�`*c���� �\0_      	   H   x�U���0Bѳ/�̌�^��K\B�5�Q�)��\L�d8��i�F:�prj�z݋��m�8�L          U	  x��ZKo#�>k~����������F��X0Ҭ�	E*��s�]ñH`H 6�UPV���/���T).�3Mq8��Z�rꫪ���1;�A�⾐=�R����)���q���a6̦��A�?�e������q6�y�x<�z�lo<��16/�,N���E�8�_����?�eq�/��8�&�?=88��lo|����O���]QD����0�`��)���uzh�1�������{'{���RP�43���﯈��x4�L�O��Av�
���y��|A��^���!
���_qZ~\>���&d^�1��X����3P����s"���\������-�ha���H�a\�e��)_�������R4�:���"~=�ZA���P!�- B���Ε��7����`���M�O
� V�`�� ��l���ad������%z�5sN��7����~��AE3V���m�Jf��]� �L���~<�OO�B���G�u鵦�	��|@��$D(.�+r�+��K~Nn���	�u�e��T>+��g�$�W~T>/����e��h#�t���x؀瘑�;�	P��5~��Ŀ)?oTO���*l�N�"rS��!ϣV���3��4lT�:z W�F�� ��}H;����n,��$u9�\������6^��?�J��� �j��pZD��ѣ�L�MJ��ǽ�/d���M��k�����]8���� r5S�'��ZPֱ�Ը;-$�0)r&�W��A���"������)P��W����^��.U�U�#+] ����R7-/�"��z_��vs+����]�M$�ڻ�h��U����.�&RڃHRVD���&�AǐR��-�f[V�#U]	� ��<og!�8����n����4tv�F:�]&����y4ivM2L�*�`(���T�=�&2L7�;mCҜ�X�}s��]���<��w��y��
�nV	�)��J��������ݭ�� �VศXn���v�`-��)�t�d�Q����k�L���)��n��Ͱ��k9Q��xI:qwv��I9�ZJ�n�2C�Z�>3uJ
WKP˥:U�q���~Z�Nq�4��P�	ZzK �ked�}Q�.�7�������W�(�H�e��e�;9������%�w�%�ϔ/res�(�Be�Жx�y����@����W���3�0V�m��qV��U�"̦J1��:2�:���O���O��뗁�P��J�0��(�@�Ȝj+��+3�
 6�n#8���Y�*�(t��tbq ��̖G����h��Qq��zD�hq�@�,QZ�Z��F�͑d�aD+Z�8!g��%�X��9p.�P���=�j�k��v�oY�%ڄre�"E����X/t��^���N�أ���k�j� �1aU��e��`�C�H��	�bIt��|��l�M��ݜ:�|�G��!��?�$�j휥X1�ͤ��X�E�.^���<��^�?$\2��:ޭ��W����)q�@!ƞ��%oHФ��N��ӂ2�s#��`�5 �Ch���F�b���(�l�NXUOF_�N�kp3_���<�f�����Vmf�(dA�X�4WE�@�1<�0��1*L[��!�.��/J?���
��(�H�r+;�����A�y��?d�{�l��$q�B����E'�q�s�\}h�������ϖ���dJ��S��>��i\��9���9l����q�6�~J73pi˘��s~H����w��~�:B�K�)T�9έ��q�O��Q���?��=-^߀uq��3���yC�������Y�M�u#���Ucݲ��(��a�?@�]!t���� _���S�M�ģ�D� �,|~�/Q���ܤBQ��j4�_F���>'�������G��ct��W��TU�[���#[�9˷z���ߍjV�P�O�؀���y�p0j}���Q6�U����(��%���c��8#S����t|8������؟ڥ���j��L;�\�5<仿�Oɸ%dȃK�CPSe?�ttd���/�����eX��.�i V�9_�Fr�`�V�W.�]��Bi],6�i^��bɟS�N3���-_�xO��`�,����_D����YE��0��tv�����r�[�Q����$�F���a��A�8V�c{����a���2�,�{~/ܞF������39�������7l�n�Ӌ��p���YP���i�[��ߐ����zT<�2��O纞�N\��#J� 9��g�� ��޼Xx�	xD�#zJ�M�l$Dr}u�w�HI�B�D�V���F�1Z�7D�]DK� ��)�s�W�Iϼ�?[�Հ����.U�K͒$�?��n      
   �   x�m�AN�@�;���w�I�_xL�\��	.���%������36"H�vg�{�����ƶ0�>�*e��y�#�<�bH��,ʹ㼳-�rEC������V�8a�60]'�q��3�k).�M�3#t8�ݏ��W|8��U�:agb���2ቖ;�{�x��(Ӽ9�b��s�u����D۰�X��v��7g|Ś����~A�N\�:ґ(_@c��*������3>�P���%���\��7���         �   x�E��
�0���S�>�4�=��M���h�LMڂ>�A��~�#�v���@'�:���0��h�l���avS�qBb�v�Sڲ����]���^`	�}��ٲ�"$� Z��1FR�����?S1C� X�r��*c���@K�#��9�R&\��%>B         =  x����N�@���H��_wi�E��'؉;Nm'A���h�қZ��B�u�B"���o�3��Qe<N�|����H�|%�d+�	�/e%~���6��� �L���1�a2��|aLN�i��H�M�����$ C�TEƒ��n�v��4���Z�鸳u�CJ�lT�Q���A��ݔg0� �A���w�)�� Q)�D�*�k=�u+���pT^-���� #�hɟ)�,h�M2�3FT�K[g�II��Dݨ��EV���=+�eoN����#��廬Nߒ8����;)��L8EF�R�J�R.T��V�<M����rI�:9!����5\�ܘ���K,DJM�P�2��Z���hƽ&y���{8�(yFŠqV��Ņ���Ӑ��:s�C
����}C�i�Z&�߂:'|���H�W��v�����6#��1Y�\�s_}$��b2�P�Q�"5/�\��P��aN�&Ùw(
̞�ֺ��.�V�� > F�<������>k��<YB�Z�)�N7���k��i�[3�	��@
v����
���O8�E.�H.���� ��x�{1I�q��.ʞ �'�����=��Ԥ+���� O��$H�|���Ͱ��m'�L�LZ_�v�A��s�K�7��|�P�J���{N�Q>5� ��ބ�3Èc�.����/�t�C�C�7�y0��ᒗ�6짲�l*�j1�L,�D��8.�q�j�h�*���d>����rݘ���^$`Y�+�^�m�6�[��	�D�OM�dh������O�JAʇ���^�k��ڿYv��@x0+�?�Ӹ         F   x�3�0I�¾�.츰�{��.��.6]�ua7Pj�r�r^���ta�ņ�`�=... @�&�         �  x�M��n�P��㧈� Q}��x ^�e7�|촴\��X�ڊ%�BJA��F�'
A�(�3�_�'aƷTG�"��73��D~�3
�.q����{��g��
�������@A�_q>�s��>��tV�!-r)�X�b��󆼨����C������6���OB��k`�i5���	��)�(��Te�
���J�ژ��h��,�+_I)@+�d�{՞`��3bm��u����|L�)�4�L��_����r�н6k��t�6b7�N��M ��l��O�������)%u�S@Q蜰3���!��%Y�ҒZ�ɓ-{�O�d��ԅ,��S%���C�n	:�[�i����h����WxM�.B��/��1������;>$��J*�X3���]��|�|�D��[ӍAo��A7f,-\^V�S�ʡ�ݾ��up�Bi_��>�xҧ�.���d����	�����IZ0U
�s5�c���� �w�ďx�\V	��%ݏ�PO���b��+���|�����z�M\�:�J��Hu]f�\�͋ԗJV��4��>��
/C�:2�R�0:A#�mXR�{�$�:9M?4gڭ����^�>J�f�5$��>�T,�-����<���+$y�.�.l��rD��:��z�x�����@q���QC��A�p^�         >   x�3�t,-���O��IU()J�+.�/*�2�O,I-B1�J��)O�D3�t�DV���� ���     